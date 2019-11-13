import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Row, Table } from 'reactstrap';

import Panel from './Panel';
import { getMemberStatisticsUpToNow } from '../actions';
import { formatDecimalWithCurrencySymbol, format } from '../../../modules/numberFormat';

export class MemberStatistics extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {

        }
    }

    componentDidUpdate() {
    }

    _renderData() {
        const money = formatDecimalWithCurrencySymbol(5830693.01);

        return (
            <tbody>
                {this._buildRow('Total Deposits', money)}
                {this._buildRow('Total PSP fees', money)}
                {this._buildRow('Total Winlost', money)}
                {this._buildRow('Total Bonus', money)}
                {this._buildRow('Total Net Winlost', money)}
            </tbody>
        );
    }

    _buildRow(title, data) {
        return (
            <tr key={title}>
                <td>{title}</td>
                <td className="text-right">{data}</td>
            </tr>
        )
    }

    render() {
        const { } = this.state;
        const { isUpToNow } = this.props;

        return (
            <Panel heading={"Member statistics (up to now)"} className={"icon-home02"}>
                <Row>
                    <Col sm={12}>
                        <span>We have 17049 members in the program</span>
                    </Col>
                    <Col sm={12}>
                        <span>8,847 Deposit / 8,202 Non-Deposit</span>
                    </Col>
                    <Col sm={12}>
                        <Table className="member-statistics-table" sm={12}>
                            {this._renderData()}
                        </Table>
                    </Col>
                </Row>
            </Panel>
        );
    }
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getMemberStatisticsUpToNow }, dispatch);
}

export default connect(null, mapDispatchToProps)(MemberStatistics);