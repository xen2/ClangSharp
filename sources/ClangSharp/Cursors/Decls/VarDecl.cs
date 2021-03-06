using System;
using ClangSharp.Interop;

namespace ClangSharp
{
    public class VarDecl : DeclaratorDecl, IRedeclarable<VarDecl>
    {
        private readonly Lazy<VarDecl> _instantiatedFromStaticDataMember;

        internal VarDecl(CXCursor handle, CXCursorKind expectedKind) : base(handle, expectedKind)
        {
            _instantiatedFromStaticDataMember = new Lazy<VarDecl>(() => TranslationUnit.GetOrCreate<VarDecl>(Handle.SpecializedCursorTemplate));
        }

        public VarDecl InstantiatedFromStaticDataMember => _instantiatedFromStaticDataMember.Value;

        public CX_StorageClass StorageClass => Handle.StorageClass;

        public CXTLSKind TlsKind => Handle.TlsKind;
    }
}
