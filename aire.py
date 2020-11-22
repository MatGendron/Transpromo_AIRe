import tweepy
import time
from tweepy import OAuthHandler


#init
consumer_key = 'L2mPv8Oxjs2mLtBEWZZiyDy0W'
consumer_secret = 'idPHQogjNLSKdo7ns8exk9ZXSbIsqcd4UsXd1vofliwj1s2iZj'
access_token = '1413867470-bWJRTSz2Ece90tG1Z5IU9pmPZQvdWR10xrWqNBL'
access_secret = '6Z0X9eJ0dLlYJj8JrqX6VcNMOOsTWpaxaFosJhiv5Xkoa'
 

auth = OAuthHandler(consumer_key, consumer_secret)
auth.set_access_token(access_token, access_secret)
 
api = tweepy.API(auth)

#################### 
####### data #######
#################### 
def go_through(user, V, stack):
    print(user.friends())
    for friend in user.friends():
        c = [(friend.screen_name, friend.followers_count)]
        if c[0] not in V:
            V += c
            stack += c
    return V,stack




user = api.get_user("Teko24370144") # source


V = [(user.screen_name, user.followers_count)] # vertices

limit = 1
l = 0
stack = V

while l < limit:
    S2 = []
    for x in stack:
        print("DEBUG  l  =  %d  // stack:  %d // x: %s"  %(l,len(stack),x[0]))
        try:
            V, S2 = go_through(api.get_user(x[0]),V,S2)
        except tweepy.RateLimitError:
            print("Python is sleeping...")
            time.sleep(15 * 60)
    l += 1
    print("STACK CHANGE")
    stack = S2
    
    
print(V)
