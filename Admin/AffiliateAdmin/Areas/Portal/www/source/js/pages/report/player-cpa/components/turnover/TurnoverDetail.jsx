import React from 'react';
import { Modal, ModalHeader, ModalBody, Table } from 'reactstrap';
import { connect } from 'react-redux';
import { setMemberCpaId } from '../../actions';
import TableHeader from './TableHeader';
import TableBody from './TableBody';
import TableFooter from './TableFooter';
import { getMaxHeight } from '../../../libs/constants';

export const TurnoverDetail = ({ turnoverDetail, memberCpaId, setMemberCpaId }) => {
    if (!memberCpaId || !turnoverDetail) {
        return null;
    }

    const maxHeight = getMaxHeight()

    const closeModal = () => {
        setMemberCpaId(null)
    }

    return (
        <Modal
            className="turnover-detail-popup-container"
            isOpen={!!memberCpaId}
            toggle={closeModal}
            backdrop="static"
            size="md"
        >
            <ModalHeader
                toggle={closeModal}
                className="modal-header"
            >
                <h5 className="modal-title">
                    <span className="iconpopup"></span>
                    Turnover CPA Detail
                </h5>
            </ModalHeader>
            <ModalBody>
                <div className="modal-body-wrapper table-responsive" style={{ maxHeight }}>
                    <Table freeze-table bordered className="mb-0">
                        <TableHeader />
                        <TableBody groups={turnoverDetail.groups} />
                        <TableFooter total={turnoverDetail.total} />
                    </Table>
                </div>
            </ModalBody>
        </Modal>
    );
}

const mapStateToProps = (state) => {
    return {
        memberCpaId: state.memberCpaId
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        setMemberCpaId: (data) => dispatch(setMemberCpaId(data))
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(TurnoverDetail);