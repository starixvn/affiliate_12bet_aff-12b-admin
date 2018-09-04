import React, { Component } from 'react';

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { format, formatDecimal } from '../../../modules/numberFormat';

export class GridData extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return(<div></div>)
    }
}

function mapStateToProps({ }) {
    return Object.assign({}, {});
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({}, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GridData);