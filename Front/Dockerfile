FROM node:10.4

WORKDIR /usr/src/app

COPY package.json .
COPY app.js .
COPY index.html .
COPY main.js .

RUN npm install

EXPOSE 80

CMD npm start