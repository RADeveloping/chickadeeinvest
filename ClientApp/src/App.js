import React, { Component } from 'react';
import ThemeProvider from './theme';
import ScrollToTop from './components/ScrollToTop';
import Router from "./routes";
export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
        <ThemeProvider>
            <ScrollToTop />
            <Router />
        </ThemeProvider>
    );
  }
}
