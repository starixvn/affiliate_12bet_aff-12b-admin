import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

export class Content extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            data: null
        }
    }

    render() {
        return (
            <div></div>
        )
    }
}

function mapStateToProps({ param }) {
    return Object.assign({}, { param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({}, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);

