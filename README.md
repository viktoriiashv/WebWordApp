### Startup without limits:
1. Start: `docker-compose up -d`

### Startup with limits (due to specific of version 3):
1. Create stack: `docker stack deploy --compose-file docker-compose.yml webappstack`
2. To remove stack : `docker stack rm webappstack`