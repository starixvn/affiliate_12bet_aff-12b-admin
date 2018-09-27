import React from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { createStore, applyMiddleware, combineReducers, compose } from "redux";
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

const store = configureStore(window.initialState);

export default class App extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <Provider store={store}>
        <AppContainer />
      </Provider>
    );
  }
}

ReactDOM.render(<App />, document.getElementById("root"));
