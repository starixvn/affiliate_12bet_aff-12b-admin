import React, { PureComponent } from 'react';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../../modules/store';
import { removeUndifineItem } from '../../../modules/utils';
import ClaimPrincipal from '../../../modules/claim';

export default class App extends PureComponent {
    constructor(props) {
        super(props)
        this.state = {
            today: new Date().toLocaleDateString('en-US'),
            param: {
                SearchBy: "",
                FromDate: null,
                Status: null,
                IsFromHome: false
            }
        };
    }

    render() {
        const globalState = Object.assign({}, removeUndifineItem(window.initialState));
        const claimsPrincipal = ClaimPrincipal.currentClaimPrincipal;
        if (window.initialState && window.initialState.param && window.initialState.param.IsFromHome) {
            window.initialState.param.IsFromHome = false;
        }
        const state = Object.assign({}, this.state, globalState, {claimsPrincipal});
        const store = configureStore(state, reducer);
        return (
            <Provider store={store}>
                <AppContainer />
            </Provider>
        );
    }
}

