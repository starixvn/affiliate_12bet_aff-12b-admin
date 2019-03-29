import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import reducer from './reducer';
import PaymentRequestDetail from './PaymentRequestDetail';
import { configureStore } from '../../../../modules/store';

const store = configureStore(window.initialState, reducer);

export default class App extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Provider store={store}>
                <PaymentRequestDetail />
            </Provider>    
        );
    }
}

ReactDOM.render(<App />, document.getElementById('root'))