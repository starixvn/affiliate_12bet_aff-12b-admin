import React, { Component } from 'react';
import ReactDOM from 'react-dom';

import AppLayout from "../../../../layouts/AppLayout";
import AssignNormalGroup from './AssignNormalGroup';

class App extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <AppLayout>
                <AssignNormalGroup />
            </AppLayout>
        );
    }
}

ReactDOM.render(<App />, document.getElementById("root"));