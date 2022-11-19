// component
import Iconify from '../../components/common/Iconify';
import {ApplicationPaths} from "../../components/api-authorization/ApiAuthorizationConstants";

// ----------------------------------------------------------------------

const getIcon = (name) => <Iconify icon={name} width={22} height={22}/>;

export const navConfig = [
    {
        title: 'dashboard',
        path: '/dashboard/app',
        icon: getIcon('eva:pie-chart-2-fill')
    },
    {
        title: 'tickets',
        path: '/dashboard/tickets',
        icon: getIcon('ant-design:folder-open-outlined')
    },
    {
        title: 'profile',
        path: ApplicationPaths.Profile,
        icon: getIcon('eva:person-fill')
    },
    {
        title: 'Logout',
        path: ApplicationPaths.LogOut,
        icon: getIcon('eva:log-out-fill'),
    }
];

export const anonymousNavConfig = [
    {
        title: 'register',
        path: ApplicationPaths.Register,
        icon: getIcon('eva:person-add-fill'),
    },
    {
        title: 'login',
        path: ApplicationPaths.Login,
        icon: getIcon('eva:lock-fill'),
    },
];

export default navConfig;
