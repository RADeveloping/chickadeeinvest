import { Navigate, Routes, Route } from 'react-router-dom';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
// layouts
import DashboardLayout from './layouts/dashboard';
import LogoOnlyLayout from './layouts/LogoOnlyLayout';
//
import User from './pages/User';
import NotFound from './pages/Page404';
import DashboardApp from "./pages/DashboardApp";

// ----------------------------------------------------------------------

const DashboardRoutes = [
    {
        path: 'app',
        requireAuth: true,
        element: <DashboardApp />
    },
    {
        path: 'user',
        requireAuth: true,
        element: <User />
    },
];

export default function Router() {
  return (
      <Routes>


          <Route path="/" element={<LogoOnlyLayout />}>

              <Route
                  path="404"
                  element={<NotFound />}
              />

              <Route
                  path="/"
                  element={<Navigate to="/dashboard/app" />}
              />

              <Route
                  path="*"
                  element={<Navigate to="/404" />}
              />
          </Route>

          <Route path="/dashboard" element={<DashboardLayout />}>
              {DashboardRoutes.map((route, index) => {
                  route.path = "/dashboard/".concat(route.path)
                  const { element, requireAuth, ...rest} = route;
                  return <Route key={index} {...rest} element={requireAuth ? <AuthorizeRoute {...rest} element={element} /> : element} />;
              })}
          </Route>
          {ApiAuthorzationRoutes.map((route, index) => {
              const { element, requireAuth, ...rest } = route;
              return <Route key={index} {...rest} element={requireAuth ? <AuthorizeRoute {...rest} element={element} /> : element} />;
          })}
      </Routes>

  );
}
