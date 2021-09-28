import React from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";
import ClaimPrincipal from "../../../../../modules/claim";
import { configureStore } from '../../../../../modules/store';
import AssignPrivateGroup from './AssignPrivateGroup';

import reducer from './reducer';

export default class App extends React.Component {
  constructor(props) {
    super(props);

    const { productList, deductionList, affInfo, claims } = window.initialState;
    const claimsPrincipal = new ClaimPrincipal(claims);
    const initState = Object.assign({}, { productList, deductionList, affInfo, claimsPrincipal })
    this.store = configureStore(initState, reducer);
  }

  render() {
    return (
      <Provider store={this.store}>
        <AssignPrivateGroup />
      </Provider>
    );
  }
}

ReactDOM.render(<App />, document.getElementById("root"));
