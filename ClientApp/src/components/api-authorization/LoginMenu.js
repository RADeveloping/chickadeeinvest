import React, { Component, Fragment } from 'react';
import { NavItem, NavLink } from 'reactstrap';
import { Link, RouterLink } from 'react-router-dom';
import authService from './AuthorizeService';
import { ApplicationPaths } from './ApiAuthorizationConstants';
import NavSection from '../NavSection';
import Iconify from '../../components/Iconify';
import { Box, Button, Drawer, Typography, Avatar, Stack } from '@mui/material';
import { styled } from '@mui/material/styles';

const getIcon = (name) => <Iconify icon={name} width={22} height={22} />;
const AccountStyle = styled('div')(({ theme }) => ({
  display: 'flex',
  alignItems: 'center',
  padding: theme.spacing(2, 2.5),
  borderRadius: Number(theme.shape.borderRadius) * 1.5,
  backgroundColor: theme.palette.grey[500_12]
}));

export class LoginMenu extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isAuthenticated: false,
      userName: null
    };
  }

  componentDidMount() {
    this._subscription = authService.subscribe(() => this.populateState());
    this.populateState();
  }

  componentWillUnmount() {
    authService.unsubscribe(this._subscription);
  }

  async populateState() {
    const [isAuthenticated, user] = await Promise.all([
      authService.isAuthenticated(),
      authService.getUser()
    ]);
    this.setState({
      isAuthenticated,
      userName: user && user.name
    });
  }

  render() {
    const { isAuthenticated, userName } = this.state;
    if (!isAuthenticated) {
      const registerPath = `${ApplicationPaths.Register}`;
      const loginPath = `${ApplicationPaths.Login}`;
      return this.anonymousView(registerPath, loginPath);
    } else {
      const profilePath = `${ApplicationPaths.Profile}`;
      const logoutPath = `${ApplicationPaths.LogOut}`;
      const logoutState = { local: true };
      return this.authenticatedView(userName, profilePath, logoutPath, logoutState);
    }
  }

  authenticatedView(userName, profilePath, logoutPath, logoutState) {
    // ----------------------------------------------------------------------

    const navConfig = [
      {
        title: 'dashboard',
        path: '/dashboard/app',
        icon: getIcon('eva:pie-chart-2-fill')
      },
      {
        title: 'profile',
        path: profilePath,
        icon: getIcon('eva:person-fill')
      },
      {
        title: 'Logout',
        path: logoutPath,
        icon: getIcon('eva:log-out-fill'),
        state: logoutState
      }
    ];

    return (
      <Fragment>
        {/* <Box sx={{ mb: 5, mx: 2.5 }}>
       <Link underline="none" component={Link} to={profilePath}>
          <AccountStyle>
            <Box sx={{ ml: 2 , mt: 2, mb: 2 }}>
              <Typography variant="subtitle2" sx={{ color: 'text.chickadeeY.main' }}>
                   {`${userName}`}
              </Typography>
            </Box>
          </AccountStyle>
          </Link>
      </Box>  */}
        <NavSection navConfig={navConfig} />
      </Fragment>
    );
  }

  anonymousView(registerPath, loginPath) {
    const anonymousViewConfig = [
      {
        title: 'register',
        path: registerPath,
        icon: getIcon('eva:person-add-fill')
      },
      {
        title: 'login',
        path: loginPath,
        icon: getIcon('eva:lock-fill')
      }
    ];

    return (
      <Fragment>
        <NavSection navConfig={anonymousViewConfig} />
      </Fragment>
    );
  }
}
