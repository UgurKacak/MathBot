# pull official base image
FROM node:13.12.0-alpine

WORKDIR /usr/src/app
 
COPY package*.json ./
 
RUN yarn install
 
COPY . .
 
EXPOSE 3005
 
CMD [ "yarn", "start" ]