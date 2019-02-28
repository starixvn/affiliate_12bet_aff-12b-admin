import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../../modules/store';

const param = window.initialState.param;
const store = configureStore({ deviceType: param.DeviceType, fromDate: param.FromDate, toDate: param.ToDate, ...window.initialState }, reducer);

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

ReactDOM.render(<App />, document.getElementById('root'));