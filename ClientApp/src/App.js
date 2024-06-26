import React, {Component} from 'react';
import ThemeProvider from './theme';
import Router from "./routes";

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <ThemeProvider>
                <Router/>
            </ThemeProvider>
        );
    }
}
