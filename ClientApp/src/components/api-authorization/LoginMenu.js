import React, { Component, Fragment } from 'react';
import authService from './AuthorizeService';
import { ApplicationPaths } from './ApiAuthorizationConstants';
import NavSection from "../nav/NavSection";
import Iconify from "../common/Iconify";

const getIcon = (name) => <Iconify icon={name} width={22} height={22} />;

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
    const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
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
        title: 'tickets',
        path: '/dashboard/tickets',
        icon: getIcon('ant-design:folder-open-outlined')
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


    return (<Fragment>
      <NavSection navConfig={navConfig} /> 
    </Fragment>);
  }

  anonymousView(registerPath, loginPath) {

    const anonymousViewConfig = [
      {
        title: 'register',
        path: registerPath,
        icon: getIcon('eva:person-add-fill'),
      },
      {
        title: 'login',
        path: loginPath,
        icon: getIcon('eva:lock-fill'),
      },
    ];


 return (<Fragment>
      <NavSection navConfig={anonymousViewConfig} /> 
    </Fragment>);
  }
}
