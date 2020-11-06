import React, { Component } from 'react';
import { Provider } from 'react-redux';
import ReactDOM from 'react-dom';

import reducer from '../core/reducers';
import { configureStore } from '../../../modules/store';
import AppContainer from '../core/components/AppContainer';

const store = configureStore(window.initialState, reducer);

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

ReactDOM.render(<App />, document.getElementById("root"));