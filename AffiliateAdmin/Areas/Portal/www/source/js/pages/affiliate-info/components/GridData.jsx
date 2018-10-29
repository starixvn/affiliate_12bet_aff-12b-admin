import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators} from 'redux';

import { Table, Button } from 'reactstrap';

import { headerText } from '../modules/constants';
import { setPagingOption, setParam } from '../actions';
import { toggleLoader } from '../../../actions/loaderAction';

export class GridData extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null,
            total: null
        }
    }

    componentDidUpdate(prevProps) {

    }

    async fetchData(param) {

    }

    _renderHeader() {
        return (
            <thead>
                <tr>
                    <th>{headerText.Index}</th>
                    <th>{headerText.Username}</th>
                    <th>{headerText.Status}</th>
                    <th>{headerText.FirstName}</th>
                    <th>{headerText.LastName}</th>
                    <th>{headerText.Language}</th>
                    <th>{headerText.Email}</th>
                    <th>{headerText.RegistrationDate}</th>
                    <th>{headerText.ApprovalDate}</th>
                    <th>{headerText.CasinoDate}</th>
                    <th>{headerText.CommissionGroup}</th>
                    <th>{headerText.BalanceBF}</th>
                    <th>{headerText.Member}</th>
                    <th>{headerText.Password}</th>
                </tr>
            </thead>
        );
    }

    _renderDataSection() {
        return(
            <tbody>
                <tr>
                    <td className="text-right">1</td>
                    <td>12BETAFF003</td>
                    <td className="text-center">Approved</td>
                    <td>12BETAFF003</td>
                    <td>12BETAFF003</td>
                    <td>English</td>
                    <td>abc@gmail.com</td>
                    <td className="text-center">12/30/2017 23:59:59</td>
                    <td className="text-center">12/31/2017 23:59:59</td>
                    <td className="text-center">12/31/2017</td>
                    <td className="text-center">Default</td>
                    <td className="text-right">2,000.00</td>
                    <td className="text-right">200</td>
                    <td className="text-center">
                        <button className="btn btn-link">
                            <span className="oi oi-pencil text-secondary"></span>
                        </button>
                    </td>
                </tr>
            </tbody>
        );
    }

    _builDataRow() {

    }
    
    render() {
        return (
            <div className="table-responsive table-striped">
                <Table>
                    {this._renderHeader()}
                    {this. _renderDataSection()}
                </Table>
            </div>
            );
    }
}

function mapStateToProps({ pagingOption, param }) {
    return Object.assign({}, { pagingOption, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({setPagingOption, toggleLoader, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GridData);
