import React, { Component } from 'react';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../../modules/store';
import ClaimPrincipal from '../../../modules/claim';

export default class App extends Component {
    constructor(props) {
        super(props);
        const param = {
            UserName: window.initialState.userName,
            IsNeedInitSearch: !!window.initialState.userName
        }
        this.state = { param }
    }

    render() {
        const claimsPrincipal = ClaimPrincipal.currentClaimPrincipal;
        const languageList = window.initialState.languageList;
        const userName = window.initialState.userName;
        const today = window.initialState.today;
        const state = Object.assign({}, this.state, {claimsPrincipal, languageList, today});
        const store = configureStore(state, reducer);
        return (
            <Provider store={store}>
                <AppContainer />
            </Provider>
        );
    }
}
