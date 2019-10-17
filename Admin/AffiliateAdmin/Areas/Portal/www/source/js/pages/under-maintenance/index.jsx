import React, { PureComponent } from 'react';
import ReactDOM from 'react-dom';
import AppLayout from '../../layouts/AppLayout';
import AppContainer from './components/AppContainer';

const { umTimeFrom, umTimeTo } = window.initialState;

export default class App extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <AppLayout>
                <AppContainer umTimeFrom={umTimeFrom} umTimeTo={umTimeTo} />
            </AppLayout>
        );
    }
}

ReactDOM.render(<App />, document.getElementById("root"));