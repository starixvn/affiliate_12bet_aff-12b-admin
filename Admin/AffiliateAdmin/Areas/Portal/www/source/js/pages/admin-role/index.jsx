import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import reducer from './reducers';
import { configureStore } from '../../modules/store';

const store = configureStore(window.initialState, reducer);

export default class App extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Provider store={store}>
            </Provider>
        );
    }
}

ReactDOM.render(<App />, document.getElementById("root"))