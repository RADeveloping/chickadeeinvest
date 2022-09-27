import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import DashboardLayout from "./layouts/dashboard";
import NotFound from './pages/Page404';
import LogoOnlyLayout from "./layouts/LogoOnlyLayout";
import {Navigate} from "react-router-dom";
const AppRoutes = [
  {
    path: '/',
    element: <Navigate to="dashboard" replace />
  },
  {
    path: '*',
    element: <Navigate to="404" replace />
  },
  {
    path: '/404',
    element: <><LogoOnlyLayout>
      <NotFound />
      </LogoOnlyLayout></>,
  },
  {
    index: true,
    path: '/dashboard/*',
    requireAuth: true,
    element: <DashboardLayout />
  },
  ...ApiAuthorzationRoutes
];

export default AppRoutes;
