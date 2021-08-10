import React, { Component } from 'react';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../../modules/store';

export default class App extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        const state = Object.assign({}, this.state, window.initialState);

        const store = configureStore(state, reducer);
        return (
            <Provider store={store}>
                <AppContainer />
            </Provider>
        );
    }
}
