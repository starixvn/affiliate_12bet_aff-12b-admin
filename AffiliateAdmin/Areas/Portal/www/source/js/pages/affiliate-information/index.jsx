import React from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { createStore, applyMiddleware, compose } from "redux";
import thunkMiddleware from "redux-thunk";

import reducer from './reducers';
import AppContainer from "./components/AppContainer";

function configureStore(initialState) {
  const enhancer = compose(
    applyMiddleware(
      thunkMiddleware // lets us dispatch() functions
    )
  );

  return createStore(reducer, initialState, enhancer);
}

const initialState = {
  leftMenuData: {}
};
const store = configureStore(initialState);

const App = () => {
  return (
    <Provider store={store}>
      <AppContainer />
    </Provider>
  );
}

ReactDOM.render(<App />, document.getElementById("root"));