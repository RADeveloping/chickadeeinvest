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
  '/weatherforecast',
  '/_configuration',
  '/.well-known',
  '/Identity',
  '/connect',
  '/ApplyDatabaseMigrations',
  '/_framework',
  '/RoleManager',
  '/UserRoles',
  '/Identity/css',
  '/api/Account',
  '/css',
  '/images',
  '/api'
];

module.exports = function (app) {
  const appProxy = createProxyMiddleware(context, {
    target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    },
  });

  app.use(appProxy);
  app.use(
    '/api',
    createProxyMiddleware({
      target,
      changeOrigin: true
    }),
  );
};
