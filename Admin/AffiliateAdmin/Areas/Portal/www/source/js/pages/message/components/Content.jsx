import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { htmlDecode } from '../../../modules/htmlHelper';
import { headerText, resource } from '../libs/constants';
import { setSelectedMessageId, setPagingOption, fetchData, setCheckedMessages, setShouldResetScrollPosition, setParam } from '../actions';
import ControlButtons from './ControlButtons';
import SelectAll from '../../common/components/SelectAll';

export class Content extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;

        if (param !== prevProps.param) {
            this.props.fetchData(param);
        }
    }

    onCheckItem = (isChecked, messageId) => {
        const newCheckedMessages = Object.assign({}, this.props.checkedMessages, { [messageId]: isChecked });

        this.props.setShouldResetScrollPosition(false);
        this.props.setCheckedMessages(newCheckedMessages);
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
                <Cell header={true} colWidth={40}></Cell>
                <Cell header={true} colWidth={40} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Creator}>{headerText.Creator}</Cell>
                <Cell header={true} colWidth={140} title={headerText.PostedDate}>{headerText.PostedDate}</Cell>
                <Cell header={true} colWidth={550} title={headerText.Message}>{headerText.Message}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Important}>{headerText.Important}</Cell>
                <Cell header={true} colWidth={100} title={headerText.UpdatedBy}>{headerText.UpdatedBy}</Cell>
                <Cell header={true} colWidth={140} title={headerText.UpdatedDate}>{headerText.UpdatedDate}</Cell>
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
                <Cell className="text-center" colSpan="8">{resource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        const creator = htmlDecode(dataItem.Creator);
        const message = htmlDecode(dataItem.Message);
        const important = dataItem.Important ? 'Yes' : 'No';
        const updatedBy = htmlDecode(dataItem.UpdatedBy);
        const textDanger = dataItem.Important ? "text-danger" : "";
        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center">
                    <input type="checkbox" checked={this.props.checkedMessages[dataItem.MessageId]}
                        onChange={(e) => this.onCheckItem(e.target.checked, dataItem.MessageId)} />
                </Cell>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-center" title={creator}>{creator}</Cell>
                <Cell className="text-center" title={dataItem.PostedDateText}>{dataItem.PostedDateText}</Cell>
                <Cell title={resource.ViewDetail}>
                    <Button color="link" className={`p-0 message-content ${textDanger}`} onClick={() => this.props.setSelectedMessageId(dataItem.MessageId)}>
                        {message}
                    </Button>
                </Cell>
                <Cell className="text-center" title={important}>{important}</Cell>
                <Cell className="text-center" title={updatedBy}>{updatedBy}</Cell>
                <Cell className="text-center" title={dataItem.UpdatedDateText}>{dataItem.UpdatedDateText}</Cell>
            </Row>
        )
    }

    render() {
        const { gridData, checkedMessages } = this.props;

        return gridData && (
            <div className="data-content mx-1">
                <ControlButtons />
                <div className="table-responsive table-striped my-2">
                    <Table
                        maxWidth={1000}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(gridData)}
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


function mapStateToProps({ pagingOption, param, checkedMessages, shouldResetScrollPosition, gridData }) {
    return Object.assign({}, { pagingOption, param, checkedMessages, shouldResetScrollPosition, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedMessageId, fetchData, setPagingOption, setCheckedMessages, setShouldResetScrollPosition, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);