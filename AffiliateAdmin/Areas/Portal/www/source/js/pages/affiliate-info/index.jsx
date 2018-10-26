import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../modules/store';

const languageList = [...window.initialState.languageList];
const language = {};
for(let i = 0; i < languageList.length; i++){
    const languageItem = languageList[i];
    language[languageItem.LanguageCode] = languageItem.LanguageName;
}

const store = configureStore({ languageList: language }, reducer);

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