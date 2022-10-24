import React, { Component } from 'react';
import ThemeProvider from './theme';
import ScrollToTop from './components/ScrollToTop';
import { BaseOptionChartStyle } from './components/chart/BaseOptionChart';
import Router from './routes';
export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <ThemeProvider>
        <ScrollToTop />
        <BaseOptionChartStyle />
        <Router />
      </ThemeProvider>
    );
  }
}
