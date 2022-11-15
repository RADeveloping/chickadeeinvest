import {Navigate, Routes, Route} from 'react-router-dom';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
// layouts
import DashboardLayout from './layouts/dashboard';
import LogoOnlyLayout from './layouts/LogoOnlyLayout';
//
import NotFound from './pages/Page404';
import DashboardApp from "./pages/DashboardApp";
import {Login} from "./components/api-authorization/Login";
import {
    LoginActions,
    LogoutActions
} from './components/api-authorization/ApiAuthorizationConstants';
import {Logout} from './components/api-authorization/Logout';
import LandingPage from "./pages/LandingPage";
import Tickets from "./pages/Tickets";
import TicketDetail from "./pages/TicketDetail";
import Search from "./pages/Search";

// ----------------------------------------------------------------------

const DashboardRoutes = [
    {
        path: 'app',
        requireAuth: true,
        element: <DashboardApp/>
    },
    {
        path: 'tickets/:id',
        requireAuth: true,
        element: <TicketDetail/>
    },
    {
        path: 'tickets',
        requireAuth: true,
        element: <Tickets/>
    },
    {
        path: 'search',
        requireAuth: true,
        element: <Search/>
    }
];

export default function Router() {
    return (
        <Routes>
            {/*
          Custom route to override the ASP.net routes
          */}

            <Route path="landing" element={<LandingPage/>}/>

            <Route path="login" element={<Login action={LoginActions.Login}/>}/>

            <Route
                path="signup"
                element={<Login action={LoginActions.Register}/>}
            />

            <Route
                path="logout"
                element={<Logout action={LogoutActions.Logout}/>}
            />

            <Route
                path="authentication/logged-out"
                element={<Navigate to="/dashboard/app"/>}
            />

            {/*
          Routes for layouts that only have logo.
          */}

            <Route path="/" element={<LogoOnlyLayout/>}>
                <Route
                    path="/"
                    element={<Navigate to="/dashboard/app"/>}
                />
                <Route
                    path="dashboard"
                    element={<Navigate to="/dashboard/app"/>}
                />
                <Route
                    path="404"
                    element={<NotFound/>}
                />
                <Route
                    path="*"
                    element={<Navigate to="/404"/>}
                />
            </Route>

            <Route path="/dashboard" element={<DashboardLayout/>}>
                {DashboardRoutes.map((route, index) => {
                    route.path = "/dashboard/".concat(route.path)
                    const {element, requireAuth, ...rest} = route;
                    return <Route key={index} {...rest}
                                  element={requireAuth ? <AuthorizeRoute {...rest} element={element}/> : element}/>;
                })}
            </Route>
            {ApiAuthorzationRoutes.map((route, index) => {
                const {element, requireAuth, ...rest} = route;
                return <Route key={index} {...rest}
                              element={requireAuth ? <AuthorizeRoute {...rest} element={element}/> : element}/>;
            })}
        </Routes>

    );
}
