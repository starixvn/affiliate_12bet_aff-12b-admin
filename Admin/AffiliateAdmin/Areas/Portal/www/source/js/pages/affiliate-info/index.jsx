import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../modules/store';
import { headerText } from './modules/constants';

const commGroupList = [...window.initialState.commGroupList];
const commGroup = {};
for (let i = 0; i < commGroupList.length; i++) {
    const commGroupItem = commGroupList[i];
    commGroup[commGroupItem.Id] = commGroupItem.Name;
}

const languageList = [...window.initialState.languageList];
const language = {};
for (let i = 0; i < languageList.length; i++) {
    const languageItem = languageList[i];
    language[languageItem.LanguageCode] = languageItem.LanguageName;
}

const filteredColumnList = Object.keys(headerText).filter(c => c !== 'UserName' && c !== 'Password')
const columnList = filteredColumnList.map(item => {
    return {
        columnKey: item,
        columnName: headerText[item],
        checked: true
    }
});

const { groupId, param } = window.initialState;

const store = configureStore({ languageList: language, commGroupList: commGroup, groupId, param, columnList }, reducer);

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

ReactDOM.render(<App />, document.getElementById("root"))