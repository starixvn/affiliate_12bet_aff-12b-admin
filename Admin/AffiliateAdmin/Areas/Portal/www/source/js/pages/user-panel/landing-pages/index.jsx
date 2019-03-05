import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../../modules/store';

let handleGetShortCode = (languageCode) =>{
    switch (languageCode)
    {
        case 'en-GB': return "eneuro";
        case 'en-US': return "en";
        case 'es-ES': return "es";
        case 'hi-IN': return "hien";
        case 'id-ID': return "id";
        case 'ja-JP': return "jp";
        case 'ko-KR': return "ko";
        case 'th-TH': return "th";
        case 'vi-VN': return "vn";
        case 'zh-CN': return "cs";
        case 'zh-TW': return "ch";
        default:
            return languageCode;
    }
};

const languageList = [...window.initialState.languageList];
const language = {};

for (let i = 0; i < languageList.length; i++) {
    const languageItem = languageList[i];
    const shortCode = handleGetShortCode(languageItem.LanguageCode);
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