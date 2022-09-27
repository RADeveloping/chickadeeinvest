import { Navigate, useRoutes } from 'react-router-dom';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';

// layouts
import DashboardLayout from './layouts/dashboard';
import LogoOnlyLayout from './layouts/LogoOnlyLayout';
//
import User from './pages/User';
import NotFound from './pages/Page404';
import DashboardApp from "./pages/DashboardApp";
import Login from "./pages/Login";
import Register from "./pages/Register";

// ----------------------------------------------------------------------

export default function Router() {
  return useRoutes([
    {
      path: 'app',
      element: <DashboardApp /> ,
    },
    {
      path: 'user',
      element: <User /> ,
    },
    {
      path: '*',
      element: <Navigate to="app" replace />,
    },
  ]);
  
}
