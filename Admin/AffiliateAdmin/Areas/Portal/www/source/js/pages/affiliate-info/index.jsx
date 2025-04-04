import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../modules/store';
import { headerText, defaultColumns } from './libs/constants';
import { convertListToMutilpleSelected, handleChangeMultipleSelected } from '../../modules/utils';
import ClaimPrincipal, { claimResource } from '../../modules/claim';

export default class App extends Component {
    constructor(props) {
        super(props);

        const { commGroupList, languageList, groupId, param } = window.initialState;
        const claimsPrincipal = ClaimPrincipal.currentClaimPrincipal;
        const isShowMemberLink = claimsPrincipal.hasWriteClaim(claimResource.Player);

        let newCommGroupList = convertListToMutilpleSelected(commGroupList, 'GroupId', 'GroupName');

        if (!!groupId) {
            newCommGroupList = handleChangeMultipleSelected(newCommGroupList, groupId.toString());
        }

        const language = {};
        for (let languageItem of languageList) {
            language[languageItem.LanguageCode] = languageItem.LanguageName;
        }

        const filteredColumnList = Object.keys(headerText).filter(c => !defaultColumns.includes(c))
        const columnList = filteredColumnList.map(item => {
            return {
                columnKey: item,
                columnName: headerText[item],
                checked: true
            }
        });

        this.store = configureStore({ languageList: language, commGroupList: newCommGroupList, groupId, param, columnList, isShowMemberLink }, reducer);

    }

    render() {
        return (
            <Provider store={this.store}>
                <AppContainer />
            </Provider>
        );
    }
}

ReactDOM.render(<App />, document.getElementById("root"))