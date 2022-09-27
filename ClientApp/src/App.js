import React, { Component } from 'react';
import ThemeProvider from './theme';
import ScrollToTop from './components/ScrollToTop';
import { BaseOptionChartStyle } from './components/chart/BaseOptionChart';
import AppRoutes from "./AppRoutes";
import {Route, Routes} from "react-router-dom";
import AuthorizeRoute from "./components/api-authorization/AuthorizeRoute";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
        <ThemeProvider>
            <ScrollToTop />
            <BaseOptionChartStyle />
            <Routes>
            {AppRoutes.map((route, index) => {
                const { element, requireAuth, ...rest } = route;
                return <Route key={index} {...rest} element={requireAuth ? <AuthorizeRoute {...rest} element={element} /> : element} />;
            })}
            </Routes>
        </ThemeProvider>
    );
  }
}
