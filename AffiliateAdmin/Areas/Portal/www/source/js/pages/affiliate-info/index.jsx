import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../modules/store';

const commGroupList = [...window.initialState.commGroupList];
const commGroup = {};
for(let i = 0; i < commGroupList.length; i++){
    const commGroupItem = commGroupList[i];
    commGroup[commGroupItem.LanguageCode] = commGroupItem.LanguageName;
}

const languageList = [...window.initialState.languageList];
const language = {};
for(let i = 0; i < languageList.length; i++){
    const languageItem = languageList[i];
    language[languageItem.LanguageCode] = languageItem.LanguageName;
}

const store = configureStore({ languageList: language, commGroupList: commGroup }, reducer);

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