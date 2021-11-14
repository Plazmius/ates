import React, { Component } from 'react';
import './custom.css'
import { AuthProvider } from './providers/AuthProvider';
import { Routes } from './routes/Routes';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <div>

        <AuthProvider>
          <Routes />
        </AuthProvider>
      </div>
    );
  }
}
