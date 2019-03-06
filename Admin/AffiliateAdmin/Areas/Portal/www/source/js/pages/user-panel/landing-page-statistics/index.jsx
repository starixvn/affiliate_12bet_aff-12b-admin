import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../../modules/store';

const shortCodeList = {
    'en-GB': 'eneuro',
    'en-US': 'en',
    'es-ES': 'es',
    'hi-IN': 'hien',
    'id-ID': 'id',
    'ja-JP': 'jp',
    'ko-KR': 'ko',
    'th-TH': 'th',
    'vi-VN': 'vn',
    'zh-CN': 'cs',
    'zh-TW': 'ch',
};

const languageList = [...window.initialState.languageList];
const language = {};

for (let i = 0; i < languageList.length; i++) {
    const languageItem = languageList[i];
    const shortCode = shortCodeList[languageItem.LanguageCode] || languageItem.LanguageCode;
    language[shortCode] = languageItem.LanguageName;
}

const store = configureStore(Object.assign({}, window.initialState, { languageList: language }), reducer);

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