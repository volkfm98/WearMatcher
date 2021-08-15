import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';

import ClothingItemsList from './components/ClothingItem/ClothingItemsList';
import { BrowserRouter, Link, Route, Switch } from 'react-router-dom';

ReactDOM.render(
  <React.StrictMode>
    <BrowserRouter>
      <Link to="/ClothingItems">ClothingItems</Link><br/>
      <Link to="/tags">tags</Link><br/>
      <Link to="/random">random</Link><br/>
      <Link to="/build">build</Link><br/>

      <Switch>
        <Route path="/ClothingItems" component={ClothingItemsList} />
        <Route path="/tags">
          <h1>Tags placeholder</h1>
        </Route>
        <Route path="/random">
          <h1>Random outfit placeholder</h1>
        </Route>
        <Route path="/build">
          <h1>Build outfit placeholder</h1>
        </Route>
      </Switch>
    </BrowserRouter>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
