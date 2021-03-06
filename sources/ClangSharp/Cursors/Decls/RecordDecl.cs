using System;
using System.Collections.Generic;
using System.Linq;
using ClangSharp.Interop;

namespace ClangSharp
{
    public class RecordDecl : TagDecl
    {
        private readonly Lazy<IReadOnlyList<FieldDecl>> _fields;

        internal RecordDecl(CXCursor handle, CXCursorKind expectedKind) : base(handle, expectedKind)
        {
            _fields = new Lazy<IReadOnlyList<FieldDecl>>(() => Decls.Where((decl) => decl is FieldDecl).Cast<FieldDecl>().ToList());
        }

        public bool IsAnonymousRecord => Handle.IsAnonymousRecordDecl;

        public IReadOnlyList<FieldDecl> Fields => _fields.Value;

        public bool IsUnion => Kind == CXCursorKind.CXCursor_UnionDecl;
    }
}
