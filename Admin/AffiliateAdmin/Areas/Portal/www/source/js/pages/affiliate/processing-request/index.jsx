import React, { Component } from 'react';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../../modules/store';
import ClaimPrincipal from '../../../modules/claim';

export default class App extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        const claimsPrincipal = ClaimPrincipal.currentClaimPrincipal;
        const state = Object.assign({}, this.state, {claimsPrincipal}, window.initialState);
        const store = configureStore(state, reducer);
        return (
            <Provider store={store}>
                <AppContainer />
            </Provider>
        );
    }
}
