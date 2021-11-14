import "xp.css/dist/98.css";
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { QueryClient, QueryClientProvider } from 'react-query'


const queryClient = new QueryClient()
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

ReactDOM.render(
  <QueryClientProvider client={queryClient}>
    <BrowserRouter basename={baseUrl}>
      <App />
    </BrowserRouter>
  </QueryClientProvider>,
  rootElement);

registerServiceWorker();

