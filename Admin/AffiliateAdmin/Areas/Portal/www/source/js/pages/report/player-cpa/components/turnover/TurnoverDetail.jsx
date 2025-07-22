import React from 'react';
import { Modal, ModalHeader, ModalBody, Table } from 'reactstrap';
import { connect } from 'react-redux';
import { setMemberCpaId } from '../../actions';
import TableHeader from './TableHeader';
import TableBody from './TableBody';
import TableFooter from './TableFooter';

export const TurnoverDetail = ({ turnoverDetail, memberCpaId, setMemberCpaId }) => {
    if (!memberCpaId || !turnoverDetail) {
        return null;
    }

    const closeModal = () => {
        setMemberCpaId(null)
    }
   
    return (
        <Modal 
            className="deal-popup-container" 
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
                <div className="modal-body-wrapper">
                    <Table bordered responsive className="mb-0">
                        <TableHeader />
                        <TableBody groups={turnoverDetail.groups}/>
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