import React, { Component } from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";

import reducer from './reducers';
import AppContainer from "./components/AppContainer";
import { configureStore } from '../../../modules/store';

const trackingCodeList = [...window.initialState.trackingCodeList];
const trackingCode = {};
for (let i = 0; i < trackingCodeList.length; i++) {
    const trakingCodeItem = trackingCodeList[i];
    trackingCode[trakingCodeItem.Name] = trakingCodeItem.Name;
}

const { param } = window.initialState;
const store = configureStore({ trackingCodeList: trackingCode, param }, reducer);

export default class App extends Component {
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
