import React, { Component } from "react";
import { connect } from 'react-redux';

import AppLayout from "../../../../layouts/AppLayout";
import Content from "./Content";
import InsertUpdateNoteForm from "./InsertUpdateNoteForm";
import Loader from '../../../../components/Loader';

export class AppContainer extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <AppLayout>
                <Loader show={this.props.isShowLoader} />
                <Content />
                <InsertUpdateNoteForm />
            </AppLayout>
        );
    }
}

function mapStateToProps({ isShowLoader }) {
    return Object.assign({}, { isShowLoader });
}

export default connect(mapStateToProps)(AppContainer);