import React from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";
import ClaimPrincipal from "../../../../modules/claim";
import { configureStore } from '../../../../modules/store';
import AssignPrivateGroup from './AssignPrivateGroup';

import reducer from './reducer';

const { productList, deductionList, affInfo, claims } = window.initialState;
const claimsPrincipal = new ClaimPrincipal(claims);
const initState = Object.assign({}, {productList, deductionList, affInfo, claimsPrincipal })
const store = configureStore(initState, reducer);

export default class App extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <Provider store={store}>
        <AssignPrivateGroup />
      </Provider>
    );
  }
}

ReactDOM.render(<App />, document.getElementById("root"));
