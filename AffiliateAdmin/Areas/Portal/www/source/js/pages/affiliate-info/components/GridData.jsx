import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators} from 'redux';


import { Button } from 'reactstrap';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { headerText } from '../modules/constants';
import { setPagingOption, setParam } from '../actions';
import { toggleLoader } from '../../../actions/loaderAction';
import { post } from '../../../modules/ajaxRequest';

export class GridData extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null,
            total: null
        }
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;

        if(param !== prevProps.param) {
            this.fetchData(param);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);
        const url = `Portal/AffiliateInfo/GetAffiliateList`;
        const response = await post(url, param);
        console.log(response);
        if(response && response.status === 200) {
            const { MainData, Total, TotalItem } = response.data;

            this.setState({ gridData: MainData, total: TotalItem });

            this.props.setPagingOption({TotalItem, PageIndex: param.PageIndex, PageSize: param.PageSize});
        }
        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return (
            <Row>
                <Cell header={true} colWidth={10} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={30} title={headerText.Username}>{headerText.Username}</Cell>
                <Cell header={true} colWidth={20} title={headerText.Status}>{headerText.Status}</Cell>
                <Cell header={true} colWidth={20} title={headerText.FirstName}>{headerText.FirstName}</Cell>
                <Cell header={true} colWidth={30} title={headerText.LastName}>{headerText.LastName}</Cell>
                <Cell header={true} colWidth={30} title={headerText.Language}>{headerText.Language}</Cell>
                <Cell header={true} colWidth={40} title={headerText.Email}>{headerText.Email}</Cell>
                <Cell header={true} colWidth={30} title={headerText.RegistrationDate}>{headerText.RegistrationDate}</Cell>
                <Cell header={true} colWidth={30} title={headerText.ApprovalDate}>{headerText.ApprovalDate}</Cell>
                <Cell header={true} colWidth={30} title={headerText.CasinoDate}>{headerText.CasinoDate}</Cell>
                <Cell header={true} colWidth={30} title={headerText.CommissionGroup}>{headerText.CommissionGroup}</Cell>
                <Cell header={true} colWidth={30} title={headerText.BalanceBF}>{headerText.BalanceBF}</Cell>
                <Cell header={true} colWidth={20} title={headerText.Member}>{headerText.Member}</Cell>
                <Cell header={true} colWidth={20} title={headerText.Password}>{headerText.Password}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];
        
        for(let i = 0; i < dataList.length; i++) {
            const dataItem = dataList[i];
            dataRows.push(this._buildDataRow(dataItem, i + 1));
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        const { languageList } = this.props;
        const status = dataItem.Status;
        const isShow = status && status.toLowerCase() == "approved";
        return (
            <Row>
                <Cell className="text-right" title={index}>{index}</Cell>
                <Cell title={dataItem.UserName}>{dataItem.UserName}</Cell>
                <Cell className="text-center" title={dataItem.Status}>{dataItem.Status}</Cell>
                <Cell title={dataItem.FirstName}>{dataItem.FirstName}</Cell>
                <Cell title={dataItem.LastName}>{dataItem.LastName}</Cell>
                <Cell title={dataItem.Language}>{languageList[dataItem.Language]}</Cell>
                <Cell title={dataItem.Email}>{dataItem.Email}</Cell>
                <Cell className="text-center" title={dataItem.RegistrationDate}>{dataItem.RegistrationDate}</Cell>
                <Cell className="text-center" title={dataItem.ApprovalDate}>{dataItem.ApprovalDate}</Cell>
                <Cell className="text-center" title={dataItem.CasinoDate}>{dataItem.CasinoDate}</Cell>
                <Cell className="text-center" title={dataItem.CommissionGroup}>{dataItem.CommissionGroup}</Cell>
                <Cell className="text-right" title={dataItem.BalanceBf}>{dataItem.BalanceBf}</Cell>
                <Cell className="text-right" title={dataItem.Member}>{dataItem.Member}</Cell>
                <Cell className="text-center" title={dataItem.Status}>
                    {
                        isShow && (<button className="btn btn-link">
                            <span className="oi oi-pencil text-secondary"></span>
                        </button>)
                    }
                </Cell>
            </Row>
        );
    }

    handlePaging = (index, size) => {
        const newPaging = {
            PageIndex: index,
            PageSize: size
        };
        this.props.setPagingOption(newPaging);

        const newParam = Object.assign({}, this.props.param, newPaging);
        this.fetchData(newParam);
    }
    
    render() {
        const { gridData, total } = this.state;

        return gridData && total && (
            <div className="table-responsive table-striped">
                <Table
                    minWidth={1000}
                    autoWidth={true}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(gridData)}
                    containerPadding={20}
                    isPaging={true}
                    pageOption={this.props.pagingOption}
                    onPaging={this.handlePaging}
                />
            </div>
        );
    }
}

    function mapStateToProps({ pagingOption, languageList, param }) {
        return Object.assign({}, { pagingOption, languageList, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({setPagingOption, toggleLoader, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GridData);
