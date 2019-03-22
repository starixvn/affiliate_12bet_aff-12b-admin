import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { createStore, applyMiddleware, compose } from 'redux';
import thunkMiddleware from 'redux-thunk';
import PaymentRequestDetail from './PaymentRequestDetail';

import reducer from './reducer';

function configureStore(initialState) {
  const enhancer = compose(
    applyMiddleware(
      thunkMiddleware // lets us dispatch() functions
    )
  );

  return createStore(reducer, initialState, enhancer);
}

const store = configureStore(window.initialState);

export class App extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <Provider store={store}>
        <PaymentRequestDetail />
      </Provider>
    );
  }
}

ReactDOM.render(<App />, document.getElementById('root'));
