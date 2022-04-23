# IIS redis session testing repo
This Repo target for testing redis session on both asp.net web app and http module.

# Get started

1. start redis container 
```bash
docker run -p 6379:6379 --name redis --restart=always -d redis
```

2. open solution in IDE(VS/Rider), then run WebApp
