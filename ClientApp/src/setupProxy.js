const { createProxyMiddleware } = require('http-proxy-middleware');
const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT
    ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`
    : env.ASPNETCORE_URLS
        // eslint-disable-next-line prettier/prettier
        ? env.ASPNETCORE_URLS.split(';')[0]
        // eslint-disable-next-line prettier/prettier
        : 'http://localhost:24088';

const context = [
  '/Identity',
  '/_framework',
  '/connect',
  '/ApplyDatabaseMigrations',
  '/RoleManager',
  '/UserRoles',
  '/Identity/css',
  '/api/Account',
  '/css',
  '/images',
  '/api/Tickets',
  '/api/Units',
  '/api/Properties',
  '/api/Units/current',
  '/api/Properties/current'
];


const auth = [
  '/_configuration',
  '/.well-known',
];

module.exports = function (app) {
  const appProxy = createProxyMiddleware(context, {
    target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive',
      "Access-Control-Allow-Origin": "*",
      "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE,PATCH,OPTIONS"
    },
  });

  const authProxy = createProxyMiddleware(auth, {
    target,
    secure: true,
    headers: {
      Connection: 'Keep-Alive',
      "Access-Control-Allow-Origin": "*",
      "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE,PATCH,OPTIONS"
    },
  });

  app.use(appProxy);
  app.use(authProxy);

  app.use(
      '/api',
      createProxyMiddleware({
        target,
        changeOrigin: true
      }),
  );

};
