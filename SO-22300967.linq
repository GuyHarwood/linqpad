<Query Kind="Statements" />

var openDocs = new [] {
	new { 
		docBatchId = 123,
		StatusDate = new DateTime(2014, 01, 01)
	},
	new {
		docBatchId = 123,
		StatusDate = new DateTime(2014, 01, 02)
	},
	new {
		docBatchId = 123,
		StatusDate = new DateTime(2014, 01, 03)
	}
	,
	new {
		docBatchId = 456,
		StatusDate = new DateTime(2014, 01, 04)
	}
};

var HARD_CODED_ID = 123;

var latestDoc = (from doc in openDocs 
				where doc.docBatchId == HARD_CODED_ID
				select doc.StatusDate).Max();

latestDoc.Dump();