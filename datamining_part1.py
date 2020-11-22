import tweepy
from tweepy import OAuthHandler
import time
import csv
import sys

def usage():
    sys.exit("Arguments du programme invalides.\nUsage : datamining_part1 <Twitter @> <fichier CSV de destination>")

if(len(sys.argv)!=3):
    usage()

twitterAt = sys.argv[1]
csvOut = sys.argv[2]

consumer_key = 'L2mPv8Oxjs2mLtBEWZZiyDy0W'
consumer_secret = 'idPHQogjNLSKdo7ns8exk9ZXSbIsqcd4UsXd1vofliwj1s2iZj'
access_token = '1413867470-bWJRTSz2Ece90tG1Z5IU9pmPZQvdWR10xrWqNBL'
access_secret = '6Z0X9eJ0dLlYJj8JrqX6VcNMOOsTWpaxaFosJhiv5Xkoa'
 
auth = OAuthHandler(consumer_key, consumer_secret)
auth.set_access_token(access_token, access_secret)
 
api = tweepy.API(auth, wait_on_rate_limit=True, wait_on_rate_limit_notify=True, compression=True)


# Fonction qui permet de contourner la sécuriter de l'API twitter
def limit_handled(cursor, user_nb):
    while True:
        try:
            yield cursor.next()
        except tweepy.RateLimitError:
            print("Currently at %d users"%(user_nb))
            time.sleep(15 * 60)
        except StopIteration:
            return

# On va fouiller et récupérer les ids de 0.1% de ses followers, soit d'environ 6217 personnes
# En récupérant seulement les identifiants de follow des utilisateurs, 
# on atteint un ratio de récupération de 75 000 followers toutes les 15 minutes. Ici, on stop à plus de 6000
# un pôle arbitraire: supposons que ceux qui s'intéressent un minimum à la politique suivent le président actuel
pole = api.get_user(twitterAt)
print(pole.screen_name)
print(pole.followers_count)

##Essaye de créer une liste de 5000 utilisateurs avec leur nombre de followers, devrait trouver un autre moyen que de récupérer l'utilisateur
##correspondant aux id pour récupérer leur nombre de followers, récupérer l'utilisateur entier pour accéder seulement au nombre de followers
##semble très inefficace
followers = []
try:
    for user in limit_handled(tweepy.Cursor(api.followers_ids, screen_name=pole.screen_name, count=5000).items(), len(followers)):
        temp = api.get_user(user)
        followers.append((user,temp.followers_count))
        if len(followers)==5000:
            break
##Except mal géré, à priori, on s'arrête au premier utilisateur avec un profil privé
except tweepy.TweepError:
    print("User error, user skipped")
print(len(followers))

try:
    with open(csvOut, 'w') as csvfile:
        writer = csv.writer(csvfile)
        writer.writerow(['id', 'NbFollower'])
        for follower in followers:
            writer.writerow(follower)
except IOError:
    print("Erreur I/O")