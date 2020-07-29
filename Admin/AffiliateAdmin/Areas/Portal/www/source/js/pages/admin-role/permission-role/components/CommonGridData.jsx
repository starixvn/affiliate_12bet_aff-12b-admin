import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText, globalResource } from '../libs/constants';
import { setPagingOption, fetchData, setCheckedItems, setShouldResetScrollPosition, setParam } from '../actions';
import SelectAll from '../../../common/components/SelectAll';

export class CommonGridData extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        const { isGeneral, param } = this.props;

        this.props.fetchData(isGeneral, param);
    }

    componentDidUpdate(prevProps) {
        const { isGeneral, param } = this.props;

        if (isGeneral !== prevProps.isGeneral) {
            this.props.fetchData(isGeneral, param);
        }
    }

    _handleCheckItem = (isChecked, id) => {
        const newCheckedItems = Object.assign({}, this.props.checkedItems, { [id]: isChecked });

        this.props.setShouldResetScrollPosition(false);
        this.props.setCheckedItems(newCheckedItems);
    }

    handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };
        const { isGeneral, param } = this.props;

        const newParam = Object.assign({}, param, newPaging);

        this.props.setPagingOption(newPaging);
        this.props.setParam(newParam);
        this.props.fetchData(isGeneral, newParam);
    };

    _renderHeader() {
        const { isGeneral } = this.props;
        const title = isGeneral ? headerText.ViewAffiliate : headerText.Language;

        return (
            <Row key="thead">
                <Cell header={true} colWidth={50}></Cell>
                <Cell header={true} colWidth={50} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={500} title={title}>{title}</Cell>
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
                    <input type="checkbox" checked={this.props.checkedItems[dataItem.Id]}
                        onChange={(e) => this._handleCheckItem(e.target.checked, dataItem.Id)} />
                </Cell>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={dataItem.Name}>{dataItem.Name}</Cell>
                <Cell className="text-center" title={headerText.View}>
                    <Button color="link p-0" onClick={() => this._handleEdit(dataItem.Id)}>
                        <span className="oi oi-pencil text-secondary"></span>
                    </Button>
                </Cell>
                <Cell className="text-center" title={headerText.Edit}>
                    <Button color="link p-0" onClick={() => this._handleEdit(dataItem.Id)}>
                        <span className="oi oi-trash text-danger"></span>
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
                    checkedList={this.props.checkedItems}
                    setCheckedList={this.props.setCheckedItems}
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


function mapStateToProps({ pagingOption, param, checkedItems, shouldResetScrollPosition, gridData, isGeneral }) {
    return Object.assign({}, { pagingOption, param, checkedItems, shouldResetScrollPosition, gridData, isGeneral });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, setPagingOption, setCheckedItems, setShouldResetScrollPosition, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(CommonGridData);