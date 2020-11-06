import React, { Component } from 'react';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../modules/store';
import { headerText } from './modules/constants';
import ClaimPrincipal from '../../modules/claim';
import { removeUndifineItem } from '../../modules/utils';



export default class App extends Component {
    constructor(props) {
        super(props);
        this.state = {
            languageList: {},
            commGroupList: {},
            groupId: null,
            param: {
                Status: null,
                IsFromHome: false
            }
        }
    }

    render() {
        const globalState = Object.assign({}, removeUndifineItem(window.initialState));
        if (window.initialState && window.initialState.param && window.initialState.param.IsFromHome) {
            window.initialState.param.IsFromHome = false;
        }

        const { groupId, param } = globalState;
        const initState = removeUndifineItem({ groupId, param });

        const claimsPrincipal = ClaimPrincipal.currentClaimPrincipal;

        const filteredColumnList = Object.keys(headerText)
            .filter(c => c !== 'UserName' && c !== 'Password')
            .filter(c => !headerText[c].renderClaim || claimsPrincipal.hasClaim(headerText[c].renderClaim))

        const columnList = filteredColumnList.map(item => {
            return {
                columnKey: item,
                columnName: headerText[item].label,
                checked: true
            }
        });

        const storeState = Object.assign({}, this.state, initState, { columnList, claimsPrincipal });

        const store = configureStore(storeState, reducer);

        return (
            <Provider store={store}>
                <AppContainer />
            </Provider>
        );
    }
}
