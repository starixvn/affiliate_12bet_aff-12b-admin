import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText, globalResource } from '../libs/constants';
import { setPagingOption, fetchData, setCheckedFunctions, setShouldResetScrollPosition, setParam } from '../actions';
import SelectAll from '../../../common/components/SelectAll';

export class General extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.props.fetchData(this.props.param);
    }

    onCheckItem = (isChecked, functionId) => {
        const newCheckedFunctions = Object.assign({}, this.props.checkedFunctions, { [functionId]: isChecked });

        this.props.setShouldResetScrollPosition(false);
        this.props.setCheckedFunctions(newCheckedFunctions);
    }

    handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };
        const newParam = Object.assign({}, this.props.param, newPaging);

        this.props.setPagingOption(newPaging);
        this.props.setParam(newParam);
    };

    _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={50}></Cell>
                <Cell header={true} colWidth={50} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={500} title={headerText.ViewAffiliate}>{headerText.ViewAffiliate}</Cell>
                <Cell header={true} colWidth={50} title={headerText.View}>{headerText.View}</Cell>
                <Cell header={true} colWidth={50} title={headerText.Edit}>{headerText.Edit}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { pagingOption } = this.props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i + startIndex));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="7">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center">
                    <input type="checkbox" checked={this.props.checkedFunctions[dataItem.FunctionId]}
                        onChange={(e) => this.onCheckItem(e.target.checked, dataItem.FunctionId)} />
                </Cell>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={dataItem.FunctionName}>{dataItem.FunctionName}</Cell>
                <Cell className="text-center" title={headerText.View}>
                    <Button color="link p-0" onClick={() => this._handleEdit(dataItem.FunctionId)}>
                        <span className="oi oi-pencil text-secondary"></span>
                    </Button>
                </Cell>
                <Cell className="text-center" title={headerText.Edit}>
                    <Button color="link p-0" onClick={() => this._handleEdit(dataItem.FunctionId)}>
                        <span className="oi oi-x text-danger"></span>
                    </Button>
                </Cell>
            </Row>
        )
    }

    render() {
        const { gridData } = this.props;

        return gridData && (
            <div className="data-content">
                <SelectAll
                    checkedList={this.props.checkedFunctions}
                    setCheckedList={this.props.setCheckedFunctions}
                    setShouldResetScrollPosition={this.props.setShouldResetScrollPosition}
                />
                <div className="table-responsive table-striped my-2">
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
                        shouldResetScrollPosition={this.props.shouldResetScrollPosition}
                    />
                </div>
            </div>
        );
    }
}


function mapStateToProps({ pagingOption, param, checkedFunctions, shouldResetScrollPosition, gridData }) {
    return Object.assign({}, { pagingOption, param, checkedFunctions, shouldResetScrollPosition, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, setPagingOption, setCheckedFunctions, setShouldResetScrollPosition, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(General);